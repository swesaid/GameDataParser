/*

Design:

1) IUserInteraction for handling input/output operations with user.
 
* ConsoleUserInteraction class will implement its methods.

2) IGameDataRepository for reading from a file the data

* GameDataFileRepository will implement its methods.

3) INameValidator for validating the names 

* FileNameValidator class will implement its methods
 - check for the null
 - check for the empty name

4) IContentValidator for checking if some file meets the certain format criterias

* JsonContentValidator will check if the file's content is a valid json content.

Ideas:

1) Reading from a Json file.

* If the Json string is null or empty we may throw ArgumentNullException or JsonException
* If the Json string is not in a valid format, we may throw JsonException 

*/