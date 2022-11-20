BeerInventiryApi

RESTful API built using .net6
***************************************
Purpose: Analyse input Beer data based on user criteria

Steps to setup project
------------------------
1.	Load solution file
2.   Check if all 7 projects are loaded
3.   Set BeerInventoryAPI as startup project
4.	Build and run
5.	Swagger url should open for test
6. 	If not, after starting the service, paste url in browser as mentioned below
7.	Can check logs at location :  C:\BeerInventoryApi\Logs. New log file created every day.


Route format in steps below
--------------------------------------------------
1.	Service hosted at	: https://localhost:7071
2.	Default route	 	: api/beerInventory
3.	HTTPGet method name	: minMax or mostBottles or exactPriced or allFunctions
4.	Possible Parameters	: 'price' and 'url'

Output json schema
---------------------------------------------------

{
  "value": [
    {
      "caption": "Somevalue",  // what type of result data as string
      "id": 1234,				//Beer id as int
      "name": "SomeValue",	//Beername as string
      "brandName": "Somestring",	//Brand name as string
      "resultId": 1234,		//Article id as int
      "price": 12,			//Price of beer as double
      "quantity": 34,			//No of bottles as double
      "pricePerUnit": 2.2		//Price per unit of beer as double
    }
]
}

current available options-
-------------------------------------------------------
	1> Get most expensive and most cheap beers

		Method Name			: minMax
		expected input from user	: 1.url pointing to beer data in json format. This should be part of url
		sample url				: https://localhost:7071/api/beerInventory/minMax?url=https%3A%2F%2Fflapotest.blob.core.windows.net%2Ftest%2FProductData.json
		output				: Beer data in json format

	2> Get beers coming in most bottles

		Method name			:mostBottles
		expected input from user	: 1.url pointing to beer data in json format. This should be part of url
		sample url				: https://localhost:7071/api/beerInventory/mostBottles?url=https%3A%2F%2Fflapotest.blob.core.windows.net%2Ftest%2FProductData.json
		output				: Beer data in json format

	3> Get beers priced exactly at the number input by user

		Mthod name				: exactPriced
		expected input from user	: 1.url pointing to beer data in json format. This should be part of url  
							  2. Price to filter
		sample url				: https://localhost:7071/api/beerInventory/exactPriced?price=19.99&url=https%3A%2F%2Fflapotest.blob.core.windows.net%2Ftest%2FProductData.json
		output				: Beer data in json format

	4> All above 3 functionalities together

		Method name			: allFunctions
		expected input from user	: 1.url pointing to beer data in json format. This should be part of url
							  2. Price to filter	
		sample url				: https://localhost:7071/api/beerInventory/allFunctions?price=19.99&url=https%3A%2F%2Fflapotest.blob.core.windows.net%2Ftest%2FProductData.json
		output				: Beer data in json format





****************************************************************
				Project structure
****************************************************************

BeerInventoryAPI consists of 7 projects

1.BeerInventoryAPI
------------------------
# Startup Project
# Has default controller for all 4 HTTPGet methods
# All startup logic including dependency injection and logging
# Service factory to inject required serviced based on user reuest

2.BeerInventryApi.Data
------------------------
# All classes and their implementation representing input and output data



3.BeerInventoryApi.Helper
------------------------
# Logic to parse string and calculate price and quantity in int and double format based on input data


4.BeerInventoryApi.Logger
------------------------
# Centralized logging mechanism
# Used external logging provider Serilog
	Reason: Easy to use and mainain


5. BeerInventoryApi.Services
----------------------------
# Service classes for each functionality criteria for analysis of input beer data
# Separate Service to get data from json
# Also contains main logic to filtre data based on user request

6.BeerInventoryApi.utility
-------------------------------
# Depenedency injection using extrenal provider Unity


7. BeerInventoryApi.Test
-------------------------------
# Test classes 
# Used inbuilt MSTest framework





****************************************************************
				About project
****************************************************************
Limitations
-----------

1.	Only important hence limited test scenarios included
2.	Only DEBUG setup done. Production settings TODO
3.	Only Default Swagger which comes with Web rest api project template is incorporated for testing
4.	Output json schema is created by my own understanding.
Note: It may look like lot of projects are used with only 1 or 2 classes. Reason is to keep the repo loosly coupled, resuable and extendable just by adding project references.



What can be improved
---------------------
1.	Use default dependency injection provider by MS. Currently Unity is used as I have been using it and I am more comfortable with it.
2. 	Use default logger provided by MS. Currently Serilog is used for ease of development.
3.	Use builder pattern for service creation. Currently Factory pattern is used