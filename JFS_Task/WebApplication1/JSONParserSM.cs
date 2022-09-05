namespace JFS_Task
{
    /// <summary>
    /// I made this state machine to better handle manual parsing of custom JSON files.
    /// </summary>
    public class JSONParserSM
    {
        public string? ArrayName { get; set; }

        public State State { get; set; }
    }

    public enum State
    {
        LookingForArray,
        LookingForObject
    }
}
