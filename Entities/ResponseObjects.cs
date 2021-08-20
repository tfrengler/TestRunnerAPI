using System;
using System.Collections.Generic;

public interface IResponseError
{
    List<string> Errors { get; init; }
}

public record StartResponseObject : IResponseError
{
    public List<string> Errors { get; init; }
    public string SessionID { get; init; }
}

public record StatusResponseObject : IResponseError
{
    public List<string> Errors { get; init; }
    public string Status { get; init; }
}