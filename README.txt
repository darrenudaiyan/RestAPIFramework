A simple skeleton rest api test framework to list Whiskies and their Assay information such as percent and specific gravity settings. The more important code is the test framework around it - rest.api.tests and the rest.api.objectmodels

To run:

Build WebApp in Visual Studio 2019
Publish in Visual Studio (as admin) + make sure you have application development features turned on in IIS)
Convert to an application in IIS Manager.
Browse to these URLS to check that it is working
http://localhost/UdaiyanRestAPI/Whiskeys
http://localhost/UdaiyanRestAPI/Assays