import { useState } from "react"
import RequestProductSendCard from "../cards/RequestProductSendCard"


const RequestProductValidation=()=> {
    const [data, setData] = useState([
        1,2,3,4,5
    ])
    return (
        <>
            {data.map((value, index)=>
                <RequestProductSendCard data={value} key={index}/>   
            )}
        </>
    )
}
export default RequestProductValidation