'use client'

import React from 'react'
import {Button, Dropdown} from "flowbite-react";
import Link from "next/link";
import {User} from "next-auth";
import {HiCog, HiUser} from "react-icons/hi2";
import {AiFillCar, AiFillTrophy, AiOutlineLogout} from "react-icons/ai";
import {DropdownDivider} from "flowbite-react/lib/esm/components/Dropdown/DropdownDivider";
import {signOut} from "next-auth/react";
import {defaultFlowbiteTheme} from "@/themes/DefaultFlowbiteTheme";

type Props = {
    user: Partial<User>
}
export default function UserActions({user}: Props) {
    if (!user) {
        return <div>Loading...</div>
    }

    return (
        <Dropdown color={'blue'} label={`Welcome ${user.name}`}>
            <Dropdown.Item icon={HiUser}>
                <Link href={'/'}>
                    My Auctions
                </Link>
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillTrophy}>
                <Link href={'/'}>
                    Auctions Won
                </Link>
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillCar}>
                <Link href={'/'}>
                    Sell my car
                </Link>
            </Dropdown.Item>
            <Dropdown.Item icon={HiCog}>
                <Link href={'/session'}>
                    Session (dev only)
                </Link>
            </Dropdown.Item>
            <DropdownDivider />
            <Dropdown.Item icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: '/'})}>
                Logout
            </Dropdown.Item>
        </Dropdown>
    )
}