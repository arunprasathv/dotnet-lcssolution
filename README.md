# dotnet-lcssolution
Download the solution and run in Visual Studio 2015.
Once the solution is able to  runn successfully go to swagger for testing scenarios. 
  Swagger End point
  http://localhost:8080/swagger
You can use Postman as well. 

End Point

POST http://localhost:8080/api/LCS/FindLCS  - will accept multiple strings and stafies all the 
given requirements.
POST http://localhost:8080/api/LCS/DynamicLCS - Will accept only two strings. Works with O(n2) 

complexity using dynamic programming.

Test Cases

Test Case 1

Request
{
  "values": [
    {
      "value": "comcast"
    },
    {
      "value": "compact"
    }
  ]
}

Expected Response

{
  "lcs": [
    {
      "value": "com"
    }
  ]
}

Test Case 2

Request

{
  "values": [
    {
      "value": "human"
    },
    {
      "value": "superman"
    },
    {
      "value": "spiderman"
    },
     {
      "value": "ironman"
    }
  ]
}

Expected Response

{
  "lcs": [
    {
      "value": "man"
    }
  ]
}


Test Case 3

Request

{
  "values": [
    {
      "value": "comcast"
    },
    {
      "value": "communication"
    },
    {
      "value": "broadcast"
    }
  ]
}
Expected Response
{
  "lcs": [
    {
      "value": "ca"
    }
  ]
}

Test Case 4

Request

{
  "values": [
    {
      "value": "commcastica"
    },
    {
      "value": "communicast"
    }
  ]
}

Expected Response

{
  "lcs": [
    {
      "value": "comm"
    },
    {
      "value": "cast"
    }
  ]

Test Case 5


Request

{
  "values": [
    {
      "value": "comcast"
    },
    {
      "value": "compact"
    },
    {
      "value": "xyz"
    }
  ]
}

Expected Response
{
  "lcs": [
    {
      "value": "No LCS Found"
    }
  ]
}


Test Case 6

Request

{
  "values": [
    {
      "value": "abcdefghj"
    },
    {
      "value": "hjfhghabc"
    },
    {
      "value": "asdfaabcsdf"
    } 
  ]
}

Response

{
  "lcs": [
    {
      "value": "abc"
    }
  ]
}
