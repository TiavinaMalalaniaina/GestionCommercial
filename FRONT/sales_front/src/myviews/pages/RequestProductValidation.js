import RequestProductValidationCard from "../cards/RequestProductValidationCard"
import { useEffect, useState } from "react"
import API_CONFIG from "src/apiconfig"


const RequestProductValidation=()=> {
    const [data, setData] = useState([])

    useEffect(() => {
      fetch(API_CONFIG.REQUESTS)
      .then(res=>res.json())
      .then(res => {
          console.log(res.data)
          setData(res.data)
      })
    }, [])

    const actualise=(index)=> {
    }


    return (
        <>
            {data.map((value, index)=>
                <RequestProductValidationCard data={value} key={index} removeData={actualise}/>
            )}
        </>
    )
}
export default RequestProductValidation
