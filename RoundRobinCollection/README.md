# Round Robin Collection Test
## Short Description
Create a test project and write tests for round-robin collection.
## Topics
 - Creating custom collection.
 - Work with testing framework: xUnit/NUnit/etc.
 - Best practices in unit testing.
## Requirements
 - Create readonly round-robin collection that implements IEnumerable.
 - This collections has constructor that accepts a sequence of elements.
 - GetEnumerator should return items infinitely.
 - 2 instances of GetEnumerator should return items as if we've used one shared instance of the enumerator.
   - Assume collection contains: 1, 2, 3, 4.
   - We've created 2 enumerators: iter1 and iter2.
   - And we have next pseudo-code:
     ```c#
         iter1.Current // will return 1;
         iter1.MoveNext()
         iter2.Current // will return 2
         iter2.MoveNext()
         iter1.Current // will return 3
         iter1.MoveNext()
         iter1.Current // will return 4
         iter1.MoveNext()
     ```
    - Test should validate main scenarios of round-robin collection usage.