# CSV Reader Test
### Short Description
Create a test project and write tests for simple CSV reader.
### Topics
 - IO/Streams.
 - Work with testing framework: xUnit/NUnit/etc.
 - Best practices in unit testing.
### Requirements
 - Create CSV reader with constructor that accepts a stream and additional options.
 - Reader should be able to parse headers that can be in CSV file.
 - Reader should have method like ReadRecord that returns key/value collection and can be used when headers are present in file or were provided via optinos.
 - Reader should have method like ReadValues that return list of values from single record. Can be used when headers aren't required or present.
 - Create test project that covers main use cases.