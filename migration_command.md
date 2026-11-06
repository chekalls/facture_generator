# appliquer les migrations (construit l'image si besoin, puis supprime le conteneur)
docker compose --profile tools run --rm migrations

# pré-construire l'image seule
docker compose --profile tools build migrations

# revenir à une migration précise (ou tout annuler avec 0)
docker compose --profile tools run --rm migrations 20261106050303_InitIdentity

# --- en local (sans Docker) ---
# créer une migration : le DbContext vit dans la couche Infrastructure
dotnet ef migrations add <Nom> --project src/FactureGenerator.Infrastructure --output-dir Persistence/Migrations

# vérifier que le modèle correspond à la dernière migration
dotnet ef migrations has-pending-model-changes --project src/FactureGenerator.Infrastructure

# appliquer en local (utilise src/FactureGenerator.Web/appsettings.Development.json)
dotnet ef database update --project src/FactureGenerator.Infrastructure
