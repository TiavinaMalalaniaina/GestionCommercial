import { useEffect, useState } from "react"
import API_CONFIG from "src/apiconfig"
import ProformaInfo from "./ProformaInfo"

const Proforma=()=> {
    const [proformas, setProforma] = useState([])

    useEffect(()=>{
      fetch(API_CONFIG.PROFORMAS)
      .then(res=>res.json())
      .then(res=>{
        console.log(res)
        setProforma(res.data)
      })
      .catch(error=>console.log(error))
    },[])

    return (
        <>
            {proformas.map((proforma, index) =>
                <ProformaInfo proforma={proforma} key={index}/>
            )}
        </>
    )
}
export default Proforma
