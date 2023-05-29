resource "azurerm_servicebus_namespace" "servicebus" {
  name                = "youinvest-servicebus-namespace"
  location            = azurerm_resource_group.group.location
  resource_group_name = azurerm_resource_group.group.name
  sku                 = "Basic"
}

resource "azurerm_servicebus_queue" "transactions-queue" {
  name         = "transactions-queue"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_queue" "cashstatement-items-queue" {
  name         = "cashstatement-items-queue"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}