public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.  The
    /// node is always added to the back of the queue regardless of 
    /// the priority.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    public string Dequeue()
    {
        if (_queue.Count == 0) // Verify the queue is not empty
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Find the index of the item with the highest priority to remove
        var highPriorityIndex = 0;
        // DEFECT FIX 1: Changed _queue.Count - 1 to _queue.Count so the last
        // element is included in the search. The original condition caused the
        // last item to always be skipped, meaning it could never be selected
        // as the highest priority even if it had the highest priority value.
        for (int index = 1; index < _queue.Count; index++)
        {
            // DEFECT FIX 2: Changed >= to > to preserve FIFO ordering for items
            // with equal priority. Using >= would replace the tracked index on a
            // tie, making a later-enqueued item win instead of the earlier one.
            if (_queue[index].Priority > _queue[highPriorityIndex].Priority)
                highPriorityIndex = index;
        }

        // Remove and return the item with the highest priority
        var value = _queue[highPriorityIndex].Value;
        // DEFECT FIX 3: Added RemoveAt so the dequeued item is actually removed
        // from the list. Without this the queue never shrinks and the same item
        // is returned on every call.
        _queue.RemoveAt(highPriorityIndex);
        return value;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}