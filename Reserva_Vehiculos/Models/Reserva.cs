using Microsoft.AspNetCore.Authorization;

namespace Reserva_Vehiculos;
[Authorize]
public class Reserva
{
   public String acep_fecha { get; set; }
   public Double valor_reserva { get; set; }
    public Reserva()
    {

    }
}
