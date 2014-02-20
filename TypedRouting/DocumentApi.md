# Document API
Ideas on an F# typed routing system leveraging 
[apiblueprint](http://apiblueprint.org).

This document serves as both documentation to the example API as well
as the format specification of the API. This document is parsed and
used to generate the definition of the API.

## List Documents [GET /docs{?skip}]
+ Parameters

    + skip (int, `100`) ... Number of results to skip.

+ Response 200 (text/plain)

        Hello World!

## Get Document [GET /docs/{id}]
+ Parameters

    + id (int, `1`) ... Id of the document to fetch.

+ Response 200 (text/plain)

        Hello World!

