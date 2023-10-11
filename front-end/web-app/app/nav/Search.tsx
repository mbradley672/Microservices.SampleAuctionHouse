'use client'

import React, {useState} from 'react'
import {FaSearch} from "react-icons/fa";
import {useParamsStore} from "@/hooks/useParamsStore";

export default function Search() {
    const setParams = useParamsStore(st =>st.setParams)
    const [value, setValue] = useState('');
    
    function onChange(event: any) {
        setValue(event.target.values)
    }
    
    function search(){
        setParams({searchTerm: value})
    }
    
    return (<>
        <div className={'flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm bg-white border-amber-300'} suppressHydrationWarning>
            <input type="text" placeholder='Search for cars by make, model or color' onChange={onChange} data-lpignore suppressHydrationWarning
                   onKeyDown={(e:any)=> {
                       if(e.key === 'Enter') search()
                   }}
                   data-np-autofill-type="search" data-np-uid="123"
            className={'flex-grow pl-5 bg-transparent focus:outline-none border-transparent focus:border-transparent focus:ring-0 text-sm text-grey-700'}/>
            <button onClick={search} suppressHydrationWarning>
                <FaSearch size={34} className={'bg-red-400 text-white rounded-full p-2 mx-2 cursor-pointer'}/>
            </button>
        </div></>
    )
}