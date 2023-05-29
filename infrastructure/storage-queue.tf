resource "azurerm_storage_account" "storageaccount" {
  name                = var.storage_account_name
  resource_group_name = azurerm_resource_group.group.name
  location = azurerm_resource_group.group.location
  account_tier = "Standard"
  account_replication_type = "LRS"
}




resource "azurerm_storage_queue" "example" {
   name                 = "youinveststoragequeue"
   storage_account_name = azurerm_storage_account.storageaccount.name
 }