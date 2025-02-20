﻿namespace ElectionGuard
{
    /// <Summary>
    /// Enumeration for the type of VoteVariationType
    /// see: https://developers.google.com/elections-data/reference/vote-variation
    /// </Summary>
    public enum VoteVariationType
    {
        /// <summary>
        /// an unknown value
        /// </summary>
        unknown = 0,
        /// <summary>
        /// Each voter can select up to one option.
        /// </summary>
        one_of_m = 1,
        /// <summary>
        /// Approval voting, where each voter can select as many options as desired.
        /// </summary>
        approval = 2,
        /// <summary>
        /// Borda count, where each voter can rank the options, and the rankings are assigned point values.
        /// </summary>
        borda = 3,
        /// <summary>
        /// Cumulative voting, where each voter can distribute their vote to up to N options.
        /// </summary>
        cumulative = 4,
        /// <summary>
        /// A 1-of-m method where the winner needs more than 50% of the vote to be elected.
        /// </summary>
        Majority = 5,
        /// <summary>
        /// A method where each voter can select up to N options.
        /// </summary>
        n_of_m = 6,
        /// <summary>
        /// A 1-of-m method where the option with the most votes is elected, regardless of 
        /// whether the option has more than 50% of the vote.
        /// </summary>
        plurality = 7,
        /// <summary>
        /// A proportional representation method, which is any system that elects winners 
        /// in proportion to the total vote. For the single transferable vote (STV) method, 
        /// use rcv instead.
        /// </summary>
        proportional = 8,
        /// <summary>
        /// Range voting, where each voter can select a score for each option.
        /// </summary>
        range = 9,
        /// <summary>
        /// Ranked choice voting (RCV), where each voter can rank the options, and the 
        /// ballots are counted in rounds. Also known as instant-runoff voting (IRV) and 
        /// the single transferable vote (STV).
        /// </summary>
        rcv = 10,
        /// <summary>
        /// A 1-of-m method where the winner needs more than some predetermined fraction 
        /// of the vote to be elected, and where the fraction is more than 50%. For example, 
        /// the winner might need three-fifths or two-thirds of the vote.
        /// </summary>
        super_majority = 11,
        /// <summary>
        /// The vote variation is a type that isn't included in this enumeration. 
        /// If used, provide the item's custom type in an OtherType element.
        /// </summary>
        other = 12
    };
}