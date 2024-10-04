// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

namespace GoldenBack.API.Constants;

/// <summary>
///     Provides a collection of constant messages used throughout the application for consistent messaging.
/// </summary>
public static class Messages
{
    /// <summary>
    ///     Message indicating that the session was not found.
    /// </summary>
    public const string SessionNotFound = "Session not found";

    /// <summary>
    ///     Message indicating an unknown error occurred during sign-up.
    /// </summary>
    public const string SignUpUnknownError = "An unknown error occurred during sign-up";

    /// <summary>
    ///     Message indicating an unknown error occurred during sign-in.
    /// </summary>
    public const string SignInUnknownError = "An unknown error occurred during sign-in";

    /// <summary>
    ///     Message indicating the session is not valid or tokens have expired.
    /// </summary>
    public const string SessionNotValid = "Session not valid or tokens expired";

    /// <summary>
    ///     Message indicating that token validation was successful.
    /// </summary>
    public const string TokenValidationSuccess = "Token validation successful";

    /// <summary>
    ///     Message indicating that logout from the device was successful.
    /// </summary>
    public const string LogoutSuccess = "Logout from this device successful";

    /// <summary>
    ///     Message indicating that logout from the device failed.
    /// </summary>
    public const string LogoutFailed = "Logout failed";

    /// <summary>
    ///     Message indicating that the creation operation was successful.
    /// </summary>
    public const string CreationSuccess = "Creation successful";

    /// <summary>
    ///     Message indicating that the creation operation failed.
    /// </summary>
    public const string CreationFailed = "Creation failed";

    /// <summary>
    ///     Message indicating that the delete operation was successful.
    /// </summary>
    public const string DeleteSuccess = "Delete successful";

    /// <summary>
    ///     Message indicating that the delete operation failed.
    /// </summary>
    public const string DeleteFailed = "Delete failed";

    /// <summary>
    ///     Message indicating that the delete operation was successful.
    /// </summary>
    public const string FetchSuccess = "Fetching successful";

    /// <summary>
    ///     Message indicating that the delete operation failed.
    /// </summary>
    public const string FetchFailed = "Fetching failed";


    /// <summary>
    ///     Message indicating that the operation was successful.
    /// </summary>
    public const string OperationSuccess = "Operation success";

    /// <summary>
    ///     Message indicating that the operation failed.
    /// </summary>
    public const string OperationFailed = "Operation failed";

    /// <summary>
    ///     Message indicating that the update operation was successful.
    /// </summary>
    public const string UpdateSuccess = "Update successful";

    /// <summary>
    ///     Message indicating that the update operation failed.
    /// </summary>
    public const string UpdateFailed = "Update failed";

    /// <summary>
    ///     Message indicating that the user is forbidden from accessing the resource.
    /// </summary>
    public const string Forbidden = "Forbidden";

    /// <summary>
    ///     Message indicating that the request lacks one or more parameter needed.
    /// </summary>
    public const string MissingParam = "Missing parameters";

    /// <summary>
    ///     Message indicating that the requested resource was not found.
    /// </summary>
    public const string NotFound = "Not found";

    /// <summary>
    ///     Message indicating that the requested resource was not found.
    /// </summary>
    public const string ServerError = "Internal server error";

    /// <summary>
    ///     Message indicating that the request does not fit expectations.
    /// </summary>
    public const string WrongFormat = "Wrong format";
}