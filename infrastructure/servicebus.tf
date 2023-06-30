resource "azurerm_servicebus_namespace" "servicebus" {
  name                = "youinvest-servicebus-namespace"
  location            = azurerm_resource_group.group.location
  resource_group_name = azurerm_resource_group.group.name
  sku                 = "Basic"
}

resource "azurerm_servicebus_queue" "stocktransactions-queue" {
  name         = "stocktransactions-queue"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_queue" "cashstatement-items-queue" {
  name         = "cashstatement-items-queue"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_queue" "stocktransactions-enriched-queue" {
  name         = "stocktransactions-enriched-queue"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_queue" "cashstatement-items-enriched-queue" {
  name         = "cashstatement-items-enriched-queue"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}