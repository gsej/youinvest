resource "azurerm_service_plan" "plan" {
  name                = "${var.service_name}plan"
  resource_group_name = azurerm_resource_group.group.name
  location            = azurerm_resource_group.group.location
  os_type             = "Linux"
  sku_name            = "S1"
}