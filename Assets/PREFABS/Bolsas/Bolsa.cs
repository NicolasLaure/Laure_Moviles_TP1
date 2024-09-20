using UnityEngine;
using System.Collections;
using BagsPool;
using UnityEngine.Serialization;

public class Bolsa : MonoBehaviour
{
    public Pallet.Valores Monto;

    //public int IdPlayer = 0;
    public string TagPlayer = "";
    public Texture2D ImagenInventario;
    Player Pj = null;

    public GameObject Particulas;
    public float particlesDuration = 2.5f;

    // Use this for initialization
    void Start()
    {
        Monto = Pallet.Valores.Valor2;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == TagPlayer)
        {
            Pj = coll.GetComponent<Player>();
            if (Pj.AgregarBolsa(this))
                Desaparecer();
        }
    }

    public void Desaparecer()
    {
        StartCoroutine(DespawnCoroutine());
    }

    private IEnumerator DespawnCoroutine()
    {
        Particulas.SetActive(true);
        yield return new WaitForSeconds(particlesDuration);
        Particulas.SetActive(false);
        BagPool.instance.TryReturnObject(gameObject);
    }
}