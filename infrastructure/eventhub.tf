
resource "azurerm_eventhub_namespace" "eventhubnamespace" {
  name                = "youinvestlEventHubNamespace"
  location            = azurerm_resource_group.group.location
  resource_group_name = azurerm_resource_group.group.name
  sku                 = "Standard"
  capacity            = 1
}

resource "azurerm_eventhub" "eventhub" {
  name                = "acceptanceTestEventHub"
  namespace_name      = azurerm_eventhub_namespace.eventhubnamespace.name
  resource_group_name = azurerm_resource_group.group.name
  partition_count     = 2
  message_retention   = 1
}
