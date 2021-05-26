# CongestionTaxCalculator

CongestionTaxCalculator is a system for configurating and providing congestion tax rates for Gothenburg traffic.

## Installation

Clone the project and open it with Visual Studio, it should automatically download the dependencies.


## Usage
Launch the API by starting the CongestionTaxCalculator.API service. You can then make requests with your client of choice, for example Postman like this:

```
POST
https://localhost:44341/calculate/tax?api-version=1.0

{
    "vehicletype": 6,
    "passagedatetimes": [
        "2013-02-08 06:00:00",
        "2013-02-08 06:16:00",
        "2013-02-08 07:30:00"
    ]
}

```
