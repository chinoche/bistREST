using System;

namespace BIST.Aplicacion
{
    public class RespuestaWebAPI<T>
    {
        /// <summary>
        /// Valor númerico que indica si hubo éxito en la operación, falla en la operación o excepción
        /// Una falla es un comportamiento negativo respecto al deseo del usuario, pero no es un error del sistema
        /// Exito = 0
        /// Falla = 1
        /// Excepción = -1
        /// </summary>
        public int Resultado { get; set; }
        
        /// <summary>
        /// Mensaje que se le mostrará al usuario en caso de éxito o falla
        /// </summary>
        public String Mensaje { get; set; }

        /// <summary>
        /// EMensaje de excepción en caso de haberse presentado una (Resultado = -1)
        /// </summary>
        public String Excepcion { get; set; }

        /// <summary>
        /// Genérico que devuelve los datos consultados. En el caso de modificaciones o guardados es vacío.
        /// </summary>
        public T Datos;

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public int Records { get; set; }
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total { get; set; }
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; }

        public object RowsObj { get; set; }

        /// <summary>
        /// Calculates the pagination.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="page">The page.</param>
        public void CalculatePagination(int rows, int totalRecords, int page)
        {

            int totalPages = totalRecords >= rows ? ((totalRecords - 1) / rows) + 1 : (totalRecords / rows) + 1;
            if (page > totalPages)
            {
                page = totalPages;
            }
            Records = totalRecords;
            Total = rows < 0 ? 1 : totalPages;
            Page = page;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RespuestaWebAPI()
        {
            Resultado = 1;
        }

        /// <summary>
        /// Mérodo para guardar auditoría en base de datos de la falla.
        /// </summary>
        /// <param name="tipoComponente"></param>
        /// <param name="nombreMetodo"></param>
        /// <param name="exception"></param>
        public void RegistrarExcepcion(Type tipoComponente, string nombreMetodo, Exception exception)
        {
            string mensajeDetallado = exception.InnerException != null ? exception.InnerException.Message : exception.Message ;
            Excepcion = mensajeDetallado;
            Resultado = -1;
        }
    }


}
