namespace Application.Common.Constants.MessageKeys;

internal static partial class MessageKeys
{
    internal static class Person
    {
        public const string NameAndSurnameSameLanguage = "name_and_surname_must_be_in_same_language";
        public const string PersonOlderThan18 = "person_must_be_older_than_18";
        public const string PinExactly11Character = "pin_must_be_exactly_11_character";
        public const string PersonExistsWithPin = "person_exists_with_pin";
    }
}