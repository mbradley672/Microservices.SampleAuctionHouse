'use client'

import {Button} from "flowbite-react";
import {useParamsStore} from "@/hooks/useParamsStore";
import {AiOutlineClockCircle, AiOutlineSortAscending} from "react-icons/ai";
import {BsFillStopCircleFill} from "react-icons/bs";
import React from "react";
import {GiFinishLine, GiFlame, GiStopwatch} from "react-icons/gi";

const pageSizeSelections: number[] = [4,8,12]

const orderButtons = [
    {
        label: 'Alphabetical',
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

const filterButtons = [
    {
        label: 'Live Auctions',
        icon: GiFlame,
        value: 'live'
    },
    {
        label: 'Ending < 6 Hours',
        icon: GiFinishLine,
        value: 'endingSoon'
    },
    {
        label: 'Completed',
        icon: GiStopwatch,
        value: 'finished'
    }
]

export default function Filters(){
    const pageSize = useParamsStore(state=>state.pageSize)
    const setParams = useParamsStore(state => state.setParams)
    const orderBy = useParamsStore(st => st.orderBy)
    const filterBy = useParamsStore(st => st.filterBy)
    return (
        <>
            <div className={'flex justify-between items-center mb-4'}>
                <div>
                    <span className={'uppercase text-sm text-gray-500 mr-2'}>Filter by</span>
                    <Button.Group>
                        {filterButtons.map(({label, icon: Icon, value}) => (
                            <Button key={value} onClick={() => setParams({filterBy: value})}
                                    color={`${filterBy === value ? 'blue' : 'grey'}`}>
                                <Icon className='mr-3 h-4 w-4'/>
                                {label}
                            </Button>
                        ))}
                    </Button.Group>
                </div>
                
                <div>
                    <span className={'uppercase text-sm text-gray-500 mr-2'}>Order by</span>
                    <Button.Group>
                        {orderButtons.map(({label, icon: Icon, value}) => (
                            <Button key={value} onClick={() => setParams({ orderBy: value})}
                                    color={`${orderBy === value ? 'blue' : 'grey'}`}>
                                <Icon className='mr-3 h-4 w-4'/>
                                {label}
                            </Button>
                        ))}
                    </Button.Group>
                </div>

                <div>
                    <span className={'uppercase text-sm text-gray-500 mr-2'}>Page Size</span>
                    <Button.Group outline>
                        {pageSizeSelections.map((value, i)=> {
                            return <Button key={value} 
                                    onClick={()=> setParams({pageSize: value})}
                                           color={`${pageSize === value ? 'blue' : 'grey'}`}
                            >
                                {value}
                            </Button>
                        })}
                    </Button.Group>
                </div>
            </div>
        </>
    )
}