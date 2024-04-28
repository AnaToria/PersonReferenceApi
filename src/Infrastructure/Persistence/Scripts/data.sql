-- Cities
delete from PersonReferenceDB.dbo.Cities;

INSERT INTO PersonReferenceDB.dbo.Cities (Name, NameEn)
VALUES
    (N'თბილისი', 'Tbilisi'),
    (N'ბათუმი', 'Batumi'),
    (N'ქუთაისი', 'Kutaisi'),
    (N'რუსთავი', 'Rustavi'),
    (N'ზუგდიდი', 'Zugdidi'),
    (N'ფოთი', 'Poti'),
    (N'გორი', 'Gori'),
    (N'თელავი', 'Telavi'),
    (N'სამტრედია', 'Samtredia'),
    (N'სენაკი', 'Senaki'),
    (N'მარნეული', 'Marneuli'),
    (N'ახალციხე', 'Akhaltsikhe'),
    (N'ხაშური', 'Khashuri'),
    (N'სიღნაღი', 'Sighnaghi'),
    (N'ოზურგეთი', 'Ozurgeti');



-- Localizations
delete from PersonReferenceDB.dbo.Localizations;

INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'non_empty', N'ka', N'ველი სავალდებულოა');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'non_empty', N'en', N'Field is required');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'city_does_not_exists', N'ka', N'ქალაქი არ მოიძებნა');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'city_does_not_exists', N'en', N'City does not exists');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'phone_already_registered', N'ka', N'ნომერი რეგისტრირებულია სხვა მომხმარებელზე');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'phone_already_registered', N'en', N'Phone number is already registered');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'must_contain_only_numeric_characters', N'ka', N'მნიშვნელობა უნდა შეიცავდეს რიცხვით სიმბოლოებს');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'must_contain_only_numeric_characters', N'en', N'Value must contain only numeric characters');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'contains_only_georgian_or_latin_characters', N'ka', N'უნდა შეიცვდეს მხოლოდ ქართულ ან მხოლოდ ლათინურ სიმბოლოებს');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'contains_only_georgian_or_latin_characters', N'en', N'Value must contain only georgian or only latin characters');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_exists_with_pin', N'ka', N'მომხმარებელი მსგავსი პირადი ნომრით უკვე რეგისტრირებულია');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_exists_with_pin', N'en', N'Person is already registered with this pin');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'pin_must_be_exactly_11_character', N'ka', N'პირადი ნომერი უნდა იყოს 11 ციფრი');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'pin_must_be_exactly_11_character', N'en', N'PIN must be 11 character');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_must_be_older_than_18', N'ka', N'მომხმარებელი უნდა იყოს 18 წელზე მეტის');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_must_be_older_than_18', N'en', N'Person must be older than 18');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'name_and_surname_must_be_in_same_language', N'ka', N'სახელი და გვარი უნდა იყოს ერთ ენაზე შევსებული');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'name_and_surname_must_be_in_same_language', N'en', N'Name and Surname must be in same language');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'between_2_and_50_character', N'ka', N'მნიშვნელობის სიგრძე უნდა იყოს 2-სა და 50-ს შორის');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'between_2_and_50_character', N'en', N'Value length must be between 2-50');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_not_exists_with_id', N'ka', N'მომხმარებელი არ მოიძებნა');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_not_exists_with_id', N'en', N'Person does not exists');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'file_type_not_allowed', N'ka', N'ფაილის ფორმატი არაა დასაშველი');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'file_type_not_allowed', N'en', N'File formati is now allowed');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_already_connected', N'ka', N'მომხმარებლები უკვე დაკავშირებულები არიან');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_already_connected', N'en', N'Persons are already connected');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_not_connected', N'ka', N'მომხარებლები არ არიან დაკავშირებული');
INSERT INTO PersonReferenceDB.dbo.Localizations (ResourceKey, LanguageCode, LocalizedValue) VALUES (N'person_not_connected', N'en', N'Persons are not connected');
