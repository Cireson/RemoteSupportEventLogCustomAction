# RemoteSupportEventLogCustomAction
Example of a Cireson Remote Support Custom Action to view a device's event logs. Full Tutorial: https://platform.cireson.com/articles/remote-support-custom-actions.html


## How to Install

> As of Remote Support version _%%_ we can make use of the Remote Support Operations feature. Make sure to have at least this version installed when uploading the custom extension. 

As a pre-requisite we only need to make sure the Remote Support platform is running and the Remote Support Operations feature set is enabled.

## Enabling Remote Support Operations Feature

1. Open up a browser and navigate to the application.
2. Go to settings -> Features.
3. Click the Remote Support Operations slider to enable this feature.
4. Hit SAVE.

## Uploading Custom Extension Demo

1. Download or clone the project 'RemoteSupportEventLogCustomAction'.
2. Open the project as a new instance of Visual Studio.
3. Build the project.
4. Upload the project with the Cireson Platform Tools.
5. Once the upload is complete, go to Settings -> Remote Support Actions.
6. On the 'Name' column filter write 'GetEventLog' and press Enter.

Make sure we see the three actions that were uploaded as part of the custom action extension. One for each type of action (e.g. Remote Action, Quick Action and One Click Action).

## Testing the Custom Extension

1. Go to Computers.
2. On the 'NetBIOS Name' column look for TST-Win10, or any device that is currently online.
3. Click on the device to open Remote Manage.
4. On the Remote Manage slideout, go to Remote Actions.
5. There should be a new category listed called Remote Custom Actions.
6. Wait for the Get-EventLog action to enable and click it.
7. A form will open asking for two parameters, one will be the Log Name of the event log we want to analyze, the second argument is the NumEntries which is how many results we want to see. Once we have this information hit 'Run'.
8. We should now see either a slideout with a list of results or a slideout with an information tag when no results are shown. 
