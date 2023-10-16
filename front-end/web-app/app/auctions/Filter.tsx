'use client'

import {Button} from "flowbite-react";
import {useParamsStore} from "@/hooks/useParamsStore";
import {AiOutlineClockCircle, AiOutlineSortAscending} from "react-icons/ai";
import {BsFillStopCircleFill} from "react-icons/bs";
import React from "react";
import { IconBase } from 'react-icons'

type Props ={
    pageSize:number
    setPageSize: (size:number) => void;
}

const pageSizeSelections: number[] = [4,8,12]

const orderButtons = [
    {
        label: 'Aplhabetical',
        icon: AiOutlineSortAscending,
        value: 'make'
    },
    {
        label: 'Ending Date',
        icon: AiOutlineClockCircle,
        value: 'endingSoon'
    },
    {
        label: 'Recently Added',
        icon: BsFillStopCircleFill,
        value: 'new'
    }
]

export default function Filters(){
    const pageSize = useParamsStore(state=>state.pageSize)
    const setParams = useParamsStore(state => state.setParams)
    const orderBy = useParamsStore(st => st.orderBy)
    return (
        <>
            <div className={'flex justify-between items-center mb-4'}>
                <div>
                    <span className={'uppercase text-sm text-gray-500 mr-2'}>Order by</span>
                    <Button.Group>
                        {orderButtons.map(({label, icon, value})=>(
                            <Button key={value} onClick={() => setParams({ orderBy: value})}
                            color={`${orderBy === value ? 'red':'grey'}`}>
                                <IconBase className='' />
                                {label}
                            </Button>
                        ))}
                    </Button.Group>
                </div>
                <div>
                    <span className={'uppercase text-sm text-gray-500 mr-2'}>Page Size</span>
                    <Button.Group outline>
                        {pageSizeSelections.map((value, i)=> {
                            return <Button key={i} 
                                    onClick={()=> setParams({pageSize: value})} 
                                    color={`${pageSize === value ? 'red' :'grey'}`}
                                    className={'focus:ring-0'}>
                                {value}
                            </Button>
                        })}
                    </Button.Group>
                </div>
            </div>
        </>
    )
}