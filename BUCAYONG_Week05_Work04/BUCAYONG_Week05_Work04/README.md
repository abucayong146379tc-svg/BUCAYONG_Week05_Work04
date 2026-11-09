# BUCAYONG_Week05_Work04

C# Windows Forms Product Form based on the instructor's attached **C# Common Controls** PDF.

## Form controls from the reference
- Barcode and Name text boxes
- Multiline Description text box
- Category combo box
- Manufactured group box with Local and Abroad radio buttons
- Active check box
- Cost, Price, and Markup (%) fields
- Cancel, Clear, and Save buttons

## Added functions
- Barcode is generated automatically.
- Save checks required fields and validates cost and price.
- Cost and price accept numeric input.
- Markup is calculated automatically.

Markup formula: `((Price - Cost) / Cost) * 100`

## Run on Windows
Install the .NET 8 SDK, open this folder in a terminal, and run:

```powershell
dotnet run
```

Windows Forms is a Windows desktop application framework.
