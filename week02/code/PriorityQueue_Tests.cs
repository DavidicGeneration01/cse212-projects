using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
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
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
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

    // Add more test cases as needed below.
    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: No defect found for this case - exception was implemented correctly.
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
    // Scenario: Enqueue 4 items with mixed priorities: ("a", 3), ("b", 1), ("c", 3), ("d", 5).
    // Dequeue all 4 items.
    // Expected Result: "d"(5) first, then "a"(3) before "c"(3) due to FIFO, then "b"(1) last
    // Defect(s) Found: >= operator broke FIFO for same-priority items ("c" was returned before "a").
    //                  Item was never removed from queue after dequeuing.
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
    // Defect(s) Found: The loop condition Count - 1 caused the loop to never run (0 iterations
    //                  for a single item), so index 0 was always returned which happened to be
    //                  correct only by coincidence. Fixed by changing to Count.
    public void TestPriorityQueue_5_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only", 7);

        Assert.AreEqual("only", priorityQueue.Dequeue());
    }

}