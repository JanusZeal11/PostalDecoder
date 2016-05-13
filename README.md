# PostalDecoder
Postal Location Decoder

This project was a Programming challenge where I needed to write a program that could decript postal ID codes (zip codes) and turn get the citys from them.  I went a step further and got the states as well.

For this project I used the USPS Web Tools interface to retrieve the nessessary data.

As it stands, any list of valid post codes.  If a zip code isn't reconized, its place in the returned list states that it is an invalid code.  The others pas through just fine.

Update:

Upon reviewing before submittal, I realized I had forgotten to add in the intended result of the task, the weather report.  So I modified the functionality slightly.  The USPS Web Tools API is now used to validate input zip codes before sending the values through an ajax call to retreave the display data.


Files to Review:

PostalDecoder/PostalDecoder.Tests/Models/USPS_PostalObjectTest.cs
PostalDecoder/PostalDecoder/Controllers/HomeControler.cs
PostalDecoder/PostalDecoder/Models/LocationObject.cs
PostalDecoder/PostalDecoder/Models/USPS_PostalObject.cs
PostalDecoder/PostalDecoder/Views/Home/index.cshtml

