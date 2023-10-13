# Transportadora
# Instructivo de Compilación y Ejecución del Proyecto


## Requisitos Previos

Asegurarse de tener instalado lo siguiente en el sistema:

1. **Asegurarse de Tener Visual Studio o el ide de su preferencia instalado**
2. **Asegurarse de Tener .NET 7.x Instalado**

## Pasos para Compilar y Ejecutar el Proyecto

1. **Clonar el Repositorio**:

 	Abre una terminal y clona el repositorio de GitHub en tu sistema local utilizando el siguiente comando:  
   	git clone https://github.com/camilolambolead/tadb_202320_ex03.git


2. **Configurar las Dependencias:**

	Asegúrate de que todas las dependencias y paquetes necesarios estén configurados. Esto se puede hacer con npm como gestor de paquetes.


3. **Configurar la Base de Datos:**

	Utiliza las migraciones de Entity Framework para configurar la base de datos. Ejecuta el siguiente comando en la terminal:


4. **Compilar Proyecto**

	En Visual studio, selecciona la opción de compilación o construcción (Build). Esto compilará el proyecto y generará los archivos ejecutables necesarios.

5. **Ejecutar el Proyecto:**

	Una vez compilado con éxito, ejecuta el proyecto desde el entorno de desarrollo.

6. **Acceder a la Aplicación:**

	Abre un navegador web y accede a la aplicación utilizando la Url proporcionada.

Detalle: En la carpeta de la solucion tambíen esta incluido el script para crear la base de datos, con registros ya proprcionados y procediminetoas almacenados.

Comentario: Lamentablemente para mi no pude solucionar el problema de inyección dependencias (del cuál hablamos por chat de teams), acepto cualquier llamado de atención o regaño de su parte, se que mi nota va ser coherente con el trabajo presentado y sus fallos. Igualmente voy seguir buscando una solución para este problema y espero poder publicar este proyecto sin fallos, porque sé que mas alla del fallo no demostré el compromiso sufuicente. Muchas gracias por su compromiso y labor. A contnuacion le comento que estrategias probé para resolver el problema, sin encontrar una solución, claro: 

- Verifiqué  que CargadorService y CargadorRepository estuvieran registrados y se correspondan con ICargadorService e ICargadorRepository.
- Verifiqué  que no hayan otros controladores en el proyecto que pudieran estar causando conflictos en la resolución de dependencias.
- Verifiqué que todas las dependencias requeridas por los servicios estén registradas correctamente. 
- Limpié y reconstruí tu proyecto. 
- Revisé que todos los controladores que requieren las dependencias implementen correctamente la inyección de dependencias en sus constructores y que no haya errores tipográficos ni problemas de nombre.
- Comprobé que la versión de ASP.NET Core y las bibliotecas que se usaron fueran compatibles entre sí y estén actualizadas.

  Agradecería cualquier tipo de indicación
