./med.sh '*' | xargs -i@ echo "INSERT INTO [UnicefVirtualWarehouse.UnicefContext].[dbo].[Products] ([Name],[ProductCategory_Id]) VALUES ('@',10)"  
echo "GO"
