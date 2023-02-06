﻿using MongoDB.Driver;

namespace ElectionGuard.UI.Lib.Services;

/// <summary>
/// static class used for constant strings used in database calls
/// </summary>
internal static class Constants
{
    public readonly static string DataType = "DataType";

    public readonly static string DesignatedId = "DesignatedId";

    public readonly static string GuardianId = "GuardianId";

    public readonly static string Id = "Id";

    public readonly static string KeyCeremonyId = "KeyCeremonyId";

    public readonly static string Name = "Name";

    public readonly static string PublicKey = "PublicKey";

    public readonly static string SoftDeleted = "SoftDeleted";

    public readonly static string State = "State";

    public readonly static string TableKeyCeremonies = "key_ceremonies";

    public readonly static string CompletedAt = "CompletedAt";

    public readonly static string JointKey = "JointKey";

    
}


/// <summary>
/// Interface for defining the basic calls for the database for a given data type.
/// Any other functions that are not generic across all of the types can be added to
/// the specific Service created by this interface.
/// </summary>
/// <typeparam name="T">Data type to use for the service</typeparam>
internal interface IDatabaseService<T>
{
    Task<long> CountByFilterAsync(FilterDefinition<T> filter, string? table = null);
    Task<List<T>> GetAllAsync(string? table = null);
    Task<List<T>> GetAllByFilterAsync(FilterDefinition<T> filter, string? table = null);
    Task<List<T>> GetAllByFieldAsync(string fieldName, object fieldValue, string? table = null);
    Task<T?> GetByIdAsync(string id, string? table = null);
    Task<T?> GetByNameAsync(string name, string? table = null);
    Task<T?> GetByFieldAsync(string fieldName, object fieldValue, string? table = null);
    Task<T> SaveAsync(T data, string? table = null);
    Task UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, string? table = null);
    FilterDefinition<T> UpdateFilter(FilterDefinition<T> filter, bool getDeleted = false);
}