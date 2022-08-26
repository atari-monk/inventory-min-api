#to copy, dependecies dlls, from build, to dlls folder in api proj, use when needed
$path = "C:\kmazanek.gmail.com\Code\inventory-min-cli-app\Inventory.Min.Api\dlls"
if (-not (Test-Path $path))
{
New-Item -Path "C:\kmazanek.gmail.com\Code\inventory-min-cli-app\Inventory.Min.Api" -Name "dlls" -ItemType "directory"
}
Copy-Item -Path "C:\kmazanek.gmail.com\Build\efcore-helper\EFCore.Helper\EFCore.Helper.dll" -Destination $path
Copy-Item -Path "C:\kmazanek.gmail.com\Build\model-helper\ModelHelper\ModelHelper.dll" -Destination $path
Copy-Item -Path "C:\kmazanek.gmail.com\Build\inventory-min-data\Inventory.Min.Data\Inventory.Min.Data.dll" -Destination $path
Copy-Item -Path "C:\kmazanek.gmail.com\Build\inventory-min-lib\Inventory.Min.Lib\Inventory.Min.Lib.dll" -Destination $path