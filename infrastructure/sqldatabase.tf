resource "azurerm_mssql_server" "sqlserver" {
  name                         = "gsej-youinvest-mssqlserver"
  resource_group_name          = azurerm_resource_group.group.name
  location                     = azurerm_resource_group.group.location
  version                      = "12.0"
  administrator_login          = "gsej"
  administrator_login_password = var.youinvest_sql_password
  minimum_tls_version          = "1.2"
}

resource "azurerm_mssql_database" "youinvest" {
  name           = "youinvest"
  server_id      = azurerm_mssql_server.sqlserver.id
  license_type   = "LicenseIncluded"
  max_size_gb    = 1
  sku_name       = "Basic"
  zone_redundant = false
}

resource "azurerm_mssql_database" "enrichment" {
  name           = "youinvest-enrichment"
  server_id      = azurerm_mssql_server.sqlserver.id
  license_type   = "LicenseIncluded"
  max_size_gb    = 1
  sku_name       = "Basic"
  zone_redundant = false
}