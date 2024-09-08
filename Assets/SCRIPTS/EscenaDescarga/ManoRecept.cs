using UnityEngine;
using System.Collections;

public class ManoRecept : ManejoPallets 
{
	public bool TengoPallet = false;
	
	void FixedUpdate () 
	{
		TengoPallet = HasBags();
	}
	
	void OnTriggerEnter(Collider other)
	{
		ManejoPallets recept = other.GetComponent<ManejoPallets>();
		if(recept != null)
		{
			Dar(recept);
		}
		
	}
	
	//---------------------------------------------------------//	
	
	public override bool Recibir(Pallet pallet)
	{
		if(!HasBags())
		{
			pallet.Portador = this.gameObject;
			base.Recibir(pallet);
			return true;
		}
		else
			return false;
	}
	
	public override void Dar(ManejoPallets receptor)
	{
		switch (receptor.tag)
		{
		case "Mano":
			if(HasBags())
			{
				if(receptor.name == "Right Hand")
				{
					if(receptor.Recibir(bags[0]))
					{
						bags.RemoveAt(0);
					}
				}
				
			}
			break;
			
		case "Cinta":
			if(HasBags())
			{
				if(receptor.Recibir(bags[0]))
				{
					bags.RemoveAt(0);
				}
			}
			break;
			
		case "Estante":
			break;
		}
	}
}
