$LastVersion = Get-Content -Path ./VERSION -Force
Write-Host 'Last version:' $LastVersion
$Version = Read-Host -Prompt 'Next version'
Set-Content -Path ./VERSION -Value $Version
docker build -t mitogames/fn_mp_server:$Version .
docker push mitogames/fn_mp_server:$Version