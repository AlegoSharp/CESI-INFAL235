$response = Invoke-WebRequest -Uri "https://localhost:44343/api/date"
$response | Get-Member
TypeName: Microsoft.PowerShell.Commands.HtmlWebResponseObject
$response.RawContent
pause