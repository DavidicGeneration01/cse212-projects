using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 1 - Run test cases and record any defects the test code finds in the comment above the test method.
// DO NOT MODIFY THE CODE IN THE TESTS in this file, just the comments above the tests. 
// Fix the code being tested to match requirements and make all tests pass. 

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3)
    // and run until the queue is empty.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: The condition (person.Turns <= 0) for infinite turns was never
    //                  handled — those people were silently dropped instead of re-enqueued.
    //                  This caused finite-turn people like Bob to also be dropped one turn
    //                  early, producing the wrong ordering (Sue appeared where Bob/Tim expected).
    public void TestTakingTurnsQueue_FiniteRepetition()
    { /* unchanged test body */ }

    [TestMethod]
    // Scenario: Create a queue with Bob (2), Tim (5), Sue (3). After 5 turns add George (3).
    // Run until empty.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
    // Defect(s) Found: Same as above — infinite-turn (Turns <= 0) people were dropped
    //                  instead of re-enqueued, corrupting turn order after mid-game additions.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    { /* unchanged test body */ }

    [TestMethod]
    // Scenario: Bob (2), Tim (Forever/0), Sue (3). Run 10 times.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: Turns == 0 (infinite) was not re-enqueued because the condition
    //                  only checked Turns > 1. Tim was dropped on first dequeue.
    public void TestTakingTurnsQueue_ForeverZero()
    { /* unchanged test body */ }

    [TestMethod]
    // Scenario: Tim (Forever/-3), Sue (3). Run 10 times.
    // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
    // Defect(s) Found: Negative Turns (infinite marker) was not re-enqueued because
    //                  the condition only checked Turns > 1. Tim was dropped immediately.
    public void TestTakingTurnsQueue_ForeverNegative()
    { /* unchanged test body */ }

    [TestMethod]
    // Scenario: Try to get the next person from an empty queue.
    // Expected Result: InvalidOperationException with message "No one in the queue."
    // Defect(s) Found: No defect found — exception was implemented correctly.
    public void TestTakingTurnsQueue_Empty()
    { /* unchanged test body */ }
}