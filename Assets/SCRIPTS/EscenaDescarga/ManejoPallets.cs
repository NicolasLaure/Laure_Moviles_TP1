using UnityEngine;
using System.Collections;

public class ManejoPallets : MonoBehaviour 
{
	protected System.Collections.Generic.List<Pallet> bags = new System.Collections.Generic.List<Pallet>();
	public ControladorDeDescarga Controlador;
	protected int Contador = 0;
	
	public virtual bool Recibir(Pallet pallet)
	{
		bags.Add(pallet);
		pallet.Pasaje();
		return true;
	}
	
	public bool HasBags()
	{
		if(bags.Count > 0)
			return true;
		else
			return false;
	}
	
	public virtual void Dar(ManejoPallets receptor)
	{
		//es el encargado de decidir si le da o no la bolsa
	}
}
