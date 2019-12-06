## 1: Logger ##
### Short Description ###
Create a simple Logger.
### Topics ###
- Designing types
- SOLID principles
### Requirements ###
- Code style
- Create a class Logger which implements the next interface:
````C#
    public interface ILogger
    {
        void Error(string message);
        void Error(Exception ex);
        void Warning(string message);
        void Info(string message);
    }
````
- Logger should be able to write logs to different destinations (console, text file, database etc.)
- If there are no logging destinations provided, logger should write logs to console
- Logger and each of logging destinations should be configurable to write logs of particular level (Error, Warning, Info)
- .NET Core

---

## 2: Logging proxy ##
### Short Description ###
Implement generic proxy which is able to log each object method invocation.
### Topics ###
- Designing types
- Reflection
- Dynamic object
### Requirements ###
- Implement class LoggingProxy which is able to log each object method invocation which implements interface T. To fit this requirement consider inheriting LoggingProxy from dynamic object.
- LoggingProxy should have public method T CreateInstance(T obj) which returns logging proxy which acts like T. To implement such a method consider using library ImpromptuInterface (but not required)
- Use logger developed at task Create Logger to write logs
### Advanced Requirements ###
- [x] Implement abstract class DynamicProxy and inherit LoggingProxy from this class

---

## 3: Csv Enumerable ##
### Short Description ###
Create custom implementation of IEnumerable which is able to iterate through records in csv file.
### Topics ###
- Designing types
- Data structures
- IDisposable
### Requirements ###
- Implement generic class CsvEnumerable which is able to iterate through csv file records
- CsvEnumerable should implement IEnumerable
- To read csv file use library CsvHelper
### Advanced Requirements ###
- [ ] Don't use CsvHelper to read records from csv file. Implement reading csv file using class StreamReader
