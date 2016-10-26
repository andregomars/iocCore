#create iis website
Set-ExecutionPolicy RemoteSigned
Import-Module WebAdministration

Function bootstrap($siteName, $poolName, $port, $sitePath, $pipelineMode)
{
    $site = Get-WebSite | where { $_.Name -eq $siteName }
    if($site -ne $null)
    {
        echo "$siteName is existed."
        Remove-Website -Name $siteName
        Remove-WebAppPool -Name $poolName
        Start-Sleep -s 3
        echo "site and pool are both removed."
    }
    
    echo "Creating site: $siteName"

    $appPool = New-WebAppPool -Name $poolName -Force
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $pipelineMode
   
    $absoluteSitePath = Resolve-Path $sitePath
    New-Website -Name $siteName -Port $port -PhysicalPath $absoluteSitePath -ApplicationPool $poolName
}

bootstrap "IocJobWebApp" "IocJobWebAppPool" "5000" "C:\inetpub\iocJobWebApp" ""

#publish asp.net
$currentPath = Get-Location
Set-Location -Path ..\iocJobWebApp
$command = 'dotnet publish --framework netcoreapp1.0 --output "C:\inetpub\iocJobWebApp" --configuration Release'
Invoke-Expression $command
Set-Location $currentPath
