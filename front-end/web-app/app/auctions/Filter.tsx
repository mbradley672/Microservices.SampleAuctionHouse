import ButtonGroup from "flowbite-react/lib/esm/components/Button/ButtonGroup";
import {Button} from "flowbite-react";

type Props ={
    pageSize:number
    setPageSize: (size:number) => void;
}

const pageSizeSelections: number[] = [4,8,12]

export default function Filters({pageSize, setPageSize}:Props){
    return (
        <>
            <div className={'flex justify-between items-center mb-4'}>
                <div>
                    <span className={'uppercase text-sm text-gray-500 mr-2'}>Page Size</span>
                    <ButtonGroup>
                        {pageSizeSelections.map((value, i)=> {
                            return <Button key={i} 
                                    onClick={()=> setPageSize(value)} 
                                    color={`${pageSize === value ? 'red' :'grey'}`}
                                    className={'focus:ring-0'}>
                                {value}
                            </Button>
                        })}
                    </ButtonGroup>
                </div>
            </div>
        </>
    )
}