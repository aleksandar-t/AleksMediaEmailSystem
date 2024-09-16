Task: Create an E-mail Notification System

The owner of the system is the company AleksMedia, and the plan is to send e-mail notifications to their more than one million clients. The service should have configurable settings and e-mail templates, and based on the settings, send e-mails.
Sending of mails is triggered by other systems via some event (the event has a predefined data set for the message) in some message queue engine. The system should be scalable and able to send a large number of mails in parallel.
On the UI side, there should be a possibility for configuration per client. Also, the system should have an admin UI that will enable the possibility for mass sending of mails for campaigns (clients with all data are in XML format with 100k+ records).
The only limit for architecture, design, mocking, and implementation is to have an accent on Microsoft technologies. The solution should be provided on GitHub.

User Story:
External system triggers e-mail sending.
Service accepts it.
Service checks for a configuration and a template.
System renders e-mail.
System sends e-mail.

SOLUTION

Workflow:

Upon login in react based web client, the page takes you to the campaing page where this foprm can be seen:
![image](https://github.com/user-attachments/assets/c8f0a30d-3fcc-41dc-8e31-380c71c63f77)
by providing full path to the file and pressing Trigger Campaign the process is starting 
Example for the XML file for mass sending is attached and used as part of the solution in ".\AleksMediaEmailSystem\Common\XmlClientsSourceFiles\DemoClientsFile.xml"
Example for the html template files are attached and used as part of the solution in ".\AleksMediaEmailSystem\Common\Templates"

The same can be tested with Swagger as workaround for the login part if no user created up in the database.
The [Authorize] is not present for testing purposes, but it's funcionality can be verified in the HealthCheck controller.
If using swagger, after login the JWT token can be obtained and placed in the Authorize section of the swagger. 

After triggering, the TriggerCampaign endpoint is calling the TriggerCampaignFromXmlFileAsync from the ICampaignService,
that further utilize the massEmailService via ProcessXmlForCampaign, where clients are obtained form the xml file, and based on their Ids their email addresses are used,
for creating the email data that are going to be published on the RabbitMQ queue.

On the other end, EmailSenderWorker is created as BackgroundService that is running asynchronously and listening and processing the queue messages in the ProcessEmailsConcurrently method.
There the queued messages are obtained in batches (100 by default, could and should be done configurable in future) and run in parralel tasks. 
A demo (SendEmailDemoAsync) where is simulates work of 2 seconds, and real email sender (SendEmailAsync) methods are created.


--- Tech wise ---

What, how and why is done: 

- To be discussed.

To be implemented given more time and real life scenario: 

- A lot of cleaning up and refactoring of the code given more time and effort

- Some suggested work on the BE siide
Global error handling, try catchs, logging.. 
Authorization on the API controllers to be activated  
Data persistance of the imported campaigns from the files
DTOS and automappers for the db models 
Caching

- Lot of work on the FE side
Langing page upon Login, some menu 
Admin management with dashboard and configurations 
User 

