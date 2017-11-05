using System;

namespace TodoListLib
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime Deadline { get; set; }
    }
}
