# Exam #1
Write 2 application in WPF (preferably MVVM but not mandatory)
These two applications shall communicate in WCF (in case lack knowledge in WCF, can be written with any appropriate communication method) 
Documentation should be added, in the code, containing a brief explanation of the various functions. 

## First App: 
1. Create a table with several columns (QTY — per your choice)
2. One of the columns shall contains an image / ICON that representing an Online / Offline connection.
3. Each row shall be displayed in a different color 
4. Right-click on a row, will allow to delete the row 
5. Deleting a Row — row shall be deleted from table and a notification shall be sent via a communication method (through WCF) to the second App. 

## Second App: 
1. Shall have several fields that will allow entering data per the table's columns that was define in App 1. 
2. Shall have an indication to Online / Offline connection. 
3. A button shall be created, pressing/clicking this button, shall send the information/data from App 2 to the table of First App. 
4. Sending the data to App 1, the data is added as additional row to the table. 
5. When a notification from app 1 (section 5 in App 1) is received, a popup message with the deleted information shall appear.
