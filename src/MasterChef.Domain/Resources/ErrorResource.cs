using System.Collections.Generic;

namespace MasterChef.Domain.Resources;

public class ErrorResource
{
    public bool Success => false;
    private List<string> Messages { get; set; }

    public ErrorResource(List<string> messages)
    {
        this.Messages = messages ?? new List<string>();
    }

    public ErrorResource(string message)
    {
        this.Messages = new List<string>();

        if (!string.IsNullOrWhiteSpace(message))
            this.Messages.Add(message);
    }
}