Shared.Utilities
================

A library made up of various small utility functions/classes.  The library also contains some experimental stuff that I've been toying round with.

Library contains:
* Generic and Projected comparers that take lambda functions for the comparison operations
* Data structures
	* Bloom filter
	* Counting bloom filter
	* Trie (Prefix tree)
* Custom attribute and resolver class to allow enumeration values to be marked with descriptions
* Extension methods
	* NameValueCollection extensions
	* DataSet extensions to determine whether or not a DataSet is populated
	* Func extensions to allow a Func delegate to be automatically retried a specified number of times
	* Log4Net extensions to allow easier logging of function arguments and exceptions
	* Primitive extensions
		* DateTime 
			* Move a DateTime to the start/end of the day
			* Convert DateTime to a file system safe string
			* Convert to Unix Time
		* Event extensions for more convenient event raising
		* Exception extensions to flatten an Exception hierarchies error messages/stack trace string
		* Reflection extensions that allow easier access to a members custom attribute metadata
		* IComparable extensions for IsOdd/IsEven/Between
		* Random extensions for generating random strings
		* StringBuilder
			* StartsWith
			* EndsWith
			* Contains
			* SubString
		* String
			* Contains/Equals ignoring case
			* Levenshtein distance
			* Format
			* Hash (using any supplied hashing algorithm)
			* Pad
			* RemoveLeadingCharacters
			* Wrap (on word boundaries to make outputting to the console easier)
	* Serialization extension methods to allow any object to be serialized/deserialized using the supplied serialization providers
* Functional (experimental)
	* Curry/Uncurry
	* Y Combinator
* Common regular expression matching functions
* Security 
	* HMAC One Time Password (RFC4226) for use in two factor authentication
	* Time-based One Time Password (RFC6238) for use in two factor authentication
* SerializationProviders that wrap all of the default .Net serializers (BinaryFormatter, DataContractSerializer, DataContractJsonSerializer, XmlSerializer) and provide a common interface for serializing/deserializing
* Threading
	* ThreadRunner class to run a delegate on a thread in a specific apartment (STA/MTA).
	* ThreadShared<T> and Padlock which allow a thread shared resource to be associated with it's lock.  If the resource is accessed outside of it's lock then an exception is generated.
* ConvertEx - Alternative to System.Convert that will return Nullable<T> if the value cannot be converted
* Insist - A whole set of assert functions. Useful for validating preconditions on method entry.
* Maybe - Implementation of the Maybe monad.
* NonNullable - Provides the opposite functionality to Nullable<T> in that it prevents a null reference.
* OperationResult - Generic class for returning success/failure information from a method
* Retry - Allows a delegate to be automatically retried upon failure
* StaticRandom - Thread-safe static class for random number generation.

		