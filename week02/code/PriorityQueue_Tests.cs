using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with priorities low(1), high(10), mid(5).
    // Dequeue all three and verify they come out in descending priority order.
    // Expected Result: "high", "mid", "low"
    // Defect(s) Found: Loop condition (_queue.Count - 1) skipped the last element,
    //                  so "mid" (added last among the losers) was never considered,
    //                  causing "mid" to be returned instead of the correct next item.
    //                  Item was never removed from the queue after dequeuing.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("high", 10);
        priorityQueue.Enqueue("mid", 5);

        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("mid", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue three items where the first two share the same priority(5)
    // and the third has a lower priority(1). Dequeue all three.
    // Expected Result: "first", "second", "third" — FIFO preserved within equal priorities.
    // Defect(s) Found: The >= operator caused the second item with the same priority
    //                  to overwrite the tracked highest-priority index, breaking FIFO
    //                  ordering and returning "second" before "first".
    //                  Item was never removed from the queue after dequeuing.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("second", 5);
        priorityQueue.Enqueue("third", 1);

        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: No defect found — exception was implemented correctly.
    public void TestPriorityQueue_3_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue 4 items with mixed priorities: ("a",3), ("b",1), ("c",3), ("d",5).
    // Dequeue all 4 items.
    // Expected Result: "d"(5) first, then "a"(3) before "c"(3) due to FIFO, then "b"(1) last.
    // Defect(s) Found: >= operator broke FIFO for same-priority items ("c" was returned
    //                  before "a"). Item was never removed from queue after dequeuing.
    public void TestPriorityQueue_4_FIFOWithinSamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 3);
        priorityQueue.Enqueue("b", 1);
        priorityQueue.Enqueue("c", 3);
        priorityQueue.Enqueue("d", 5);

        Assert.AreEqual("d", priorityQueue.Dequeue());
        Assert.AreEqual("a", priorityQueue.Dequeue());
        Assert.AreEqual("c", priorityQueue.Dequeue());
        Assert.AreEqual("b", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue a single item and dequeue it.
    // Expected Result: That item's value is returned without error.
    // Defect(s) Found: Loop condition (_queue.Count - 1) caused 0 iterations for a
    //                  single-item queue, meaning index 0 was returned only by
    //                  coincidence. Fixed by changing loop bound to _queue.Count.
    public void TestPriorityQueue_5_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only", 7);

        Assert.AreEqual("only", priorityQueue.Dequeue());
    }
}