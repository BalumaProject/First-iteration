
using System;
using System.Text;
using BalumaProjectGenNHibernate.CEN.BalumaProject;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Exceptions;
using BalumaProjectGenNHibernate.EN.BalumaProject;
using BalumaProjectGenNHibernate.Exceptions;

namespace BalumaProjectGenNHibernate.CAD.BalumaProject
{
public partial class AdministradorCAD : BasicCAD, IAdministradorCAD
{
public AdministradorCAD() : base ()
{
}

public AdministradorCAD(ISession sessionAux) : base (sessionAux)
{
}



public AdministradorEN ReadOIDDefault (string NIF)
{
        AdministradorEN administradorEN = null;

        try
        {
                SessionInitializeTransaction ();
                administradorEN = (AdministradorEN)session.Get (typeof(AdministradorEN), NIF);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is BalumaProjectGenNHibernate.Exceptions.ModelException)
                        throw ex;
                throw new BalumaProjectGenNHibernate.Exceptions.DataLayerException ("Error in AdministradorCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return administradorEN;
}


public string CrearAdministrador (AdministradorEN administrador)
{
        try
        {
                SessionInitializeTransaction ();
                if (administrador.Producto != null) {
                        administrador.Producto = (BalumaProjectGenNHibernate.EN.BalumaProject.ProductoEN)session.Load (typeof(BalumaProjectGenNHibernate.EN.BalumaProject.ProductoEN), administrador.Producto.IdProducto);

                        administrador.Producto.Administrador.Add (administrador);
                }
                if (administrador.Pedido != null) {
                        administrador.Pedido = (BalumaProjectGenNHibernate.EN.BalumaProject.PedidoEN)session.Load (typeof(BalumaProjectGenNHibernate.EN.BalumaProject.PedidoEN), administrador.Pedido.IdPedido);

                        administrador.Pedido.Administrador.Add (administrador);
                }

                session.Save (administrador);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is BalumaProjectGenNHibernate.Exceptions.ModelException)
                        throw ex;
                throw new BalumaProjectGenNHibernate.Exceptions.DataLayerException ("Error in AdministradorCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return administrador.NIF;
}
}
}
