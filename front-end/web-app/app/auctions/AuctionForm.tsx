'use client'

import React, {useEffect} from 'react'
import {FieldValue, FieldValues, useForm} from "react-hook-form";
import {Button, TextInput} from "flowbite-react";
import CustomInput from "@/app/components/CustomInput";
import CustomDateInput from "@/app/components/CustomDateInput";

export default function AuctionForm() {
    const {control, register, handleSubmit, setFocus, 
        formState: {isSubmitting, isValid, isDirty, errors}} = useForm({
        mode: 'onTouched'
    });
    
    useEffect(()=> {
        setFocus('make')
    }, [setFocus])

    function onSubmit(data: FieldValues) {
        console.log(data)
    }
    
    return (
        <form className={'flex flex-col mt-3'} onSubmit={handleSubmit(onSubmit)}>
            <CustomInput label={'Make'} name={'make'} control={control} 
                         rules={{required: 'Make is required'}} />
            <CustomInput label={'Model'} name={'model'} control={control} 
                         rules={{required: 'Model is required'}} />
            <CustomInput label={'Color'} name={'color'} control={control} 
                         rules={{required: 'Color is required'}} />
            <div className="grid grid-cols-2 gap-3">
                <CustomInput label={'Year'} name={'year'} control={control} 
                             rules={{required: 'Year is required'}} type={'number'} />
                <CustomInput label={'Mileage'} name={'mileage'} control={control} 
                             rules={{required: 'Mileage is required'}} type={'number'} />
            </div>
            <CustomInput label={'Image Url'} name={'imageUrl'} control={control} 
                         rules={{required: 'Url is required'}}/>
            <div className="grid grid-cols-2 gap-3">
                <CustomInput label={'Reserve Price'} name={'reservePrice'} control={control} 
                             rules={{required: 'Reserve Price is required'}} type={'number'} />
                <CustomDateInput label={'Auction End Date'} name={'mileage'} control={control} 
                                 rules={{required: 'Auction End Date is required'}} 
                                 dateFormat={'dd MMMM yyyy h:mm a'} showTimeSelect />
            </div>
            <div className="flex justify-between">
                <Button outline color={'gray'}>Cancel</Button>
                <Button isProcessing={isSubmitting} 
                        disabled={!isValid} type={'submit'}
                        outline color={'success'}>Submit</Button>
            </div>
        </form>
    )
}