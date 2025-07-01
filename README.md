# API BACK END
##Comands Code First!
1: If you create a new migration use the below command:
```bash
     Add-Migration <MigrationName>
```

1: If already exists a migration and you want to update the database use the below command:
```bash
     Update-DataBase
```

## Especifications
.NET 8.0
Entity Framework Core 8.0
JWT

##Git Hub
2. **Creación de una nueva rama:**
   - Crea una nueva rama para tu funcionalidad bajo la siguiente nomenclatura: `feature/*nombre-de-la-rama*`. Por ejemplo, si estás trabajando en la funcionalidad de registro de solicitudes, nombra tu rama como `feature/registro-solicitudes`.
```bash
     git checkout -b feature/nombre-de-la-rama
```

3. **Realiza tus cambios:**
   - Trabaja en tu rama y realiza los cambios necesarios en el código.

4. **Realiza commits:**
   - Traer los ultimos cambios de la rama principal
```bash
     git pull origin main
```
   - Al finalizar tu trabajo, asegúrate de hacer un commit de tus cambios. Usa mensajes claros y descriptivos para tus commits:
```bash
     git add .
     git commit -m "Descripción clara de los cambios realizados"
```