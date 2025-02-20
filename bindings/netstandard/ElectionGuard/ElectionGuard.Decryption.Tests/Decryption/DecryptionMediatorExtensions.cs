using ElectionGuard.Decryption.Decryption;
using ElectionGuard.Decryption.Tests.Tally;
using ElectionGuard.ElectionSetup;
using ElectionGuard.Decryption.ChallengeResponse;
using ElectionGuard.Decryption.Shares;

namespace ElectionGuard.Decryption.Tests.Decryption;

public static class DecryptionMediatorExtensions
{
    /// <summary>
    /// Run the decryption process
    /// </summary>
    public static DecryptionResult RunDecryptionProcess(
    this DecryptionMediator mediator,
        TestDecryptionData data,
        List<Guardian> quorumOfGuardians,
        bool tallyOnly = false)
    {
        return tallyOnly
            ? mediator.RunDecryptionTallyOnly(data, quorumOfGuardians)
            : mediator.RunDecryptionProcessWithBallots(data, quorumOfGuardians);
    }

    public static DecryptionResult RunDecryptionTallyOnly(
    this DecryptionMediator mediator, TestDecryptionData data, List<Guardian> quorumOfGuardians)
    {
        // Create Shares
        foreach (var guardian in quorumOfGuardians)
        {
            var share = guardian.ComputeDecryptionShare(
                data.CiphertextTally);
            mediator.SubmitShare(share!);
        }

        return VerifyProofsAndDecrypt(mediator, data, quorumOfGuardians);
    }

    public static DecryptionResult RunDecryptionProcessWithBallots(
    this DecryptionMediator mediator, TestDecryptionData data, List<Guardian> quorumOfGuardians)
    {
        var spoiledBallots = data.CiphertextBallots.Where(i => i.IsSpoiled).ToList();
        var challengedBallots = data.CiphertextBallots.Where(i => i.IsChallenged).ToList();
        var plaintextChallengedBallots = data.PlaintextBallots
            .Where(i => data.CiphertextTally.ChallengedBallotIds.Contains(i.ObjectId))
            .Select(i => i.ToTallyBallot(data.CiphertextTally)).ToList();

        // Add spoiled ballots
        foreach (var ballot in spoiledBallots)
        {
            if (ballot.IsChallenged
            || (ballot.IsSpoiled && !data.CiphertextTally.SpoiledBallotIds.Contains(ballot.ObjectId)))
            {
                mediator.AddBallot(data.CiphertextTally.TallyId, ballot);
            }
        }

        // Create Shares
        foreach (var guardian in quorumOfGuardians)
        {
            var shares = guardian.ComputeDecryptionShares(
                data.CiphertextTally, challengedBallots);
            mediator.SubmitShares(shares, challengedBallots);
        }

        return VerifyProofsAndDecrypt(mediator, data, quorumOfGuardians);
    }

    private static DecryptionResult VerifyProofsAndDecrypt(
        DecryptionMediator mediator,
        TestDecryptionData data,
        List<Guardian> quorumOfGuardians)
    {
        // create challenges
        var challenges = mediator.CreateChallenge(data.CiphertextTally.TallyId);
        foreach (var guardian in quorumOfGuardians)
        {
            var challenge = challenges[guardian.GuardianId];
            var share = mediator.GetShare(data.CiphertextTally.TallyId, guardian.GuardianId)!;

            // respond to challenge
            var response = guardian.ComputeChallengeResponse(share, challenge);
            mediator.SubmitResponse(data.CiphertextTally.TallyId, response!);
        }

        // validate the responses
        var validated = mediator.ValidateResponses(data.CiphertextTally.TallyId);
        Assert.That(validated, Is.True);

        // decrypt the tally
        var result = mediator.Decrypt(data.CiphertextTally.TallyId);

        return result;
    }
}
