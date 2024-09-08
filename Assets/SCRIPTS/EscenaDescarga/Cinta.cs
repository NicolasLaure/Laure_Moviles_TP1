using UnityEngine;

public class Cinta : ManejoPallets 
{
	public bool Encendida;//lo que hace la animacion
	public float Velocidad = 1;
	public float Tiempo = 0.5f;
	Transform ObjAct = null;
	
	//animacion de parpadeo
	public float Intervalo = 0.7f;
	public float Permanencia = 0.2f;
	float AnimTempo = 0;
	public GameObject ModelCinta;
	public Color32 ColorParpadeo;
	Color32 ColorOrigModel;
	
	//------------------------------------------------------------//
	
	void Start () 
	{
		ColorOrigModel = ModelCinta.GetComponent<Renderer>().material.color;
	}
	
	void Update () 
	{
		//animacion de parpadeo
		if(Encendida)
		{
			AnimTempo += T.GetDT();
			if(AnimTempo > Permanencia)
			{
				if(ModelCinta.GetComponent<Renderer>().material.color == ColorParpadeo)
				{
					AnimTempo = 0;
					ModelCinta.GetComponent<Renderer>().material.color = ColorOrigModel;
				}
			}
			if(AnimTempo > Intervalo)
			{
				if(ModelCinta.GetComponent<Renderer>().material.color == ColorOrigModel)
				{
					AnimTempo = 0;
					ModelCinta.GetComponent<Renderer>().material.color = ColorParpadeo;
				}
			}
		}
		
		//movimiento del pallet
		for(int i = 0; i < bags.Count; i++)
		{
			if(bags[i].GetComponent<Renderer>().enabled)
			{
				if(!bags[i].GetComponent<Pallet>().EnSmoot)
				{
					bags[i].GetComponent<Pallet>().enabled = false;
					bags[i].TempoEnCinta += T.GetDT();
					
					bags[i].transform.position += transform.right * Velocidad * T.GetDT();
					Vector3 vAux = bags[i].transform.localPosition;
					vAux.y = 3.61f;//altura especifica
					bags[i].transform.localPosition = vAux;					
					
					if(bags[i].TempoEnCinta >= bags[i].TiempEnCinta)
					{
						bags[i].TempoEnCinta = 0;
						ObjAct.gameObject.SetActive(false);
					}
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		ManejoPallets recept = other.GetComponent<ManejoPallets>();
		if(recept != null)
		{
			Dar(recept);
		}
	}
	
	
	//------------------------------------------------------------//

	public override bool Recibir(Pallet p)
	{
        Controlador.LlegadaPallet(p);
        p.Portador = this.gameObject;
        ObjAct = p.transform;
        base.Recibir(p);
        Apagar();

        return true;
    }
	
	public void Encender()
	{
		Encendida = true;
		ModelCinta.GetComponent<Renderer>().material.color = ColorOrigModel;
	}
	public void Apagar()
	{
		Encendida = false;
		ModelCinta.GetComponent<Renderer>().material.color = ColorOrigModel;
	}
}
