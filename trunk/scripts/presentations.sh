bash med.sh '*' '*' 2 | awk -F'\t+' '{print $1}' | xargs -i@ echo "INSERT INTO [UnicefVirtualWarehouse.UnicefContext].[dbo].[Presentations] ([Name]) VALUES ('@')"  
echo "GO"
